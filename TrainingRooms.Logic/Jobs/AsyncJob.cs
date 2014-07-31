using System;
using System.Threading.Tasks;
using UpdateControls;
using UpdateControls.Fields;

namespace TrainingRooms.Logic.Jobs
{
    public interface IAsyncJob<TOutput>
    {
        TOutput Output { get; }
    }

    public static class Job
    {
        public static Trigger<TInput> Observe<TInput>(Func<TInput> trigger)
        {
            return new Trigger<TInput>(trigger);
        }

        public class Trigger<TInput>
        {
            private readonly Func<TInput> _trigger;

            public Trigger(Func<TInput> trigger)
            {
                _trigger = trigger;
            }

            public Calculation<TInput, TOutput> ComputeAsync<TOutput>(Func<TInput, Task<TOutput>> calculation)
            {
                return new Calculation<TInput, TOutput>(_trigger, calculation);
            }
        }

        public class Calculation<TInput, TOutput>
        {
            private readonly Func<TInput> _trigger;
            private readonly Func<TInput, Task<TOutput>> _calculation;

            public Calculation(Func<TInput> trigger, Func<TInput, Task<TOutput>> calculation)
            {
                _trigger = trigger;
                _calculation = calculation;
            }

            public IAsyncJob<TOutput> StartAtDefault()
            {
                return new AsyncJob<TInput, TOutput>(default(TOutput), _trigger, _calculation);
            }

            public IAsyncJob<TOutput> StartAt(TOutput initial)
            {
                return new AsyncJob<TInput, TOutput>(initial, _trigger, _calculation);
            }
        }

        class AsyncJob<TInput, TOutput> : IUpdatable, IAsyncJob<TOutput>
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
}
