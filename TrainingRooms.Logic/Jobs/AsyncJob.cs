using System;
using System.Threading.Tasks;
using UpdateControls;
using UpdateControls.Fields;

namespace TrainingRooms.Logic.Jobs
{
    public class AsyncJob<TInput, TOutput> : IUpdatable
    {
        private readonly Func<TInput> _trigger;
        private readonly Func<TInput, Task<TOutput>> _calculation;

        private Dependent<TInput> _input;
        private Task _lastTask = Task.FromResult(0);
        private Independent<TOutput> _output;

        public AsyncJob(TOutput initial, Func<TInput> trigger, Func<TInput, Task<TOutput>> calculation)
        {
            _output = new Independent<TOutput>(initial);
            _trigger = trigger;
            _calculation = calculation;

            _input = new Dependent<TInput>(trigger);
            _input.Invalidated += () => UpdateScheduler.ScheduleUpdate(this);
            UpdateNow();
        }

        public TOutput Output
        {
            get
            {
                lock (this)
                {
                    return _output;
                }
            }
        }

        public void UpdateNow()
        {
            _lastTask = Execute(_input);
        }

        private async Task Execute(TInput input)
        {
            await _lastTask;
            var output = await _calculation(input);
            lock (this)
            {
                _output.Value = output;
            }
        }
    }
}
