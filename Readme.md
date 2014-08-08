The training room application is a series of apps that all share a common model. The admin app runs on Windows and is used to schedule events in rooms. The device apps run on iOS, Android, and Windows Phone, and display the event that is scheduled for a specific room. The lobby sign app is a web page that displays a map of the venue populated with upcoming events. The Correspondence Collaboration Framework synchronizes the shared model across devices. 

# The Model
![](https://raw.githubusercontent.com/michaellperry/TrainingRooms/master/TrainingRooms.Model/Models.png)

A Correspondence model is shared among all of the devices. It is composed of related facts. A fact represents one historical entity, change, or decision. Facts cannot be deleted, and their key cannot be modified. However, new facts can be created that change the meaning of existing facts.

Facts are ubiquitous. If two devices each create a fact having the same key as the other, then they both have an instance of the same fact. This is how devices communicate.

Facts are published to one another. A device subscribes to a set of facts to tell the distributor which subset of the model it is interested in. The distributor pushes the facts published to that predecessor, in addition to all of their predecessors on down the tree. It stops when it reaches another point of publication.

The training room model has facts representing venues, rooms, groups, and events.

## Venue
A venue is a place (like the Dallas office) where groups meet for events. It has rooms and groups. Correspondence forces you into a multi-tenant model, since you cannot query for all facts of a given type. A venue represents a tenant.

## Venue Token
To ensure that all devices are sharing data, they need to have access to the same Venue fact. But Venue is unique. So if each device were to create a Venue, they would end up with different ones.

The VenueToken fact, on the other hand, has an identifier. Devices can each create a VenueToken using the same identifier, and therefore end up with the same fact.

VenueToken has a mutable property that points to the Venue. Devices have to be patient, however. When they first create their VenueToken, its Venue will be empty. They need to subscribe to the VenueToken, and then the Venue will be sent to them. If a device jumps the gun and creates a new Venue, and then assigns it to the VenueToken, it will create a conflict within that property. The property will have more than one candidate value.

## Groups
Groups belong to a venue. If a group happens to meet at a different venue from time to time, and both venues happen to use the system, then the group would be represented twice. That's the trade off that allows a group to be controlled by the venues administrator.

A group is published to a venue, so that a device that subscribes to the venue will receive all of its groups. It has mutable properties for the name and logo of the group. Please see the test OtherAdminCanSeeGroups.

A group can also be deleted. Deleting a group means creating a new GroupDelete fact. This fact will cause the isDeleted query to return true, which in turn cause the venue's groups query to skip the deleted group.

## Rooms
Rooms belong to a venue. It is published to the venue so that subscribers see all rooms. They have names, and can be deleted. Please see the tests CanNameARoom and SignCanSeeRooms. 

A sign device will subscribe to the specific room that it represents. The admin device will instead subscribe to all rooms. Please see the tests SignCanSeeEventsInSelectedRoom, SignCannotSeeEventsInUnselectedRoom, and OtherAdminCanSeeGroups.

## Schedules
A schedule is created for each day and room. This naturally subdivides history so that a subscriber doesn't get a whole bunch of historical events that they don't care about anymore. Schedules are created asynchronously on each device, not queried from the Room. Schedules are only published to the Room so that they will not be sent to all subscribers of the Venue.

## Events
An event has mutable properties, so that events can be moved to different rooms, days, and times. You assign an Event to a room on a day by creating an EventSchedule. If the event is already scheduled, the new EventSchedule should refer to the one that it supersedes. Please see the test CanScheduleAnEvent. 

# Device Logic
The TrainingRooms.Logic assembly is a portable class library containing business logic for manipulating the model. It is shared among all devices: admin, sign, and lobby.

## Device, Admin Device, and Sign Device
The Device base class represents a single device. It creates the Correspondence "Community", which is the facade for local storage and network communication. The Device creates the VenueToken, which it uses to get access to the Venue. In the current form, it uses a hard-coded identifier to ensure that all devices have the same VenueToken. In the future, they will exchange venue tokens via QR codes.

The Device base class subscribes to all of the facts in the model that it is interested in. It subscribes to the VenueToken in order to receive the venue reference. It subscribes to the Venue to receive the rooms and groups. It subscribes to the current Schedules for its set of rooms. And finally it subscribes to each of the Events occurring within those Schedules.

The AdminDevice and SignDevice derived classes determine the set of Schedules that the device is interested in. For an admin device, it will be all schedules for all rooms on the selected date. For a sign device, it will be all schedules for a single room for today and tomorrow. The schedules are created in a background job. 

## Schedule Creator
Data binding and async do not mix. Data binding is done entirely through properties, which cannot be asynchronous. On the other hand, properties (or the view models that contain them) fire PropertyChanged notifications when they are finished loading asynchronously. The schedule creator bridges the gap between asynchronous process and bindable properties.

Ordinarily in Correspondence (if you can actually call Correspondence ordinary), view models use queries to find facts that have previously been added. When the view model accesses the query, it immediately returns an empty collection. It then asynchronously starts loading the query from local storage, and fires PropertyChanged when it's finished. This allows the UI to load quickly, and the data to be filled in as it's fetched from storage. And it usually works pretty well.

In the case of the Schedule fact, however, this does not work. A Schedule is not "unique", but instead defines a key made up completely of other facts. That means that if you create two Schedules for a given Room and Day, you will have actually received two references to the same schedule. This is a useful property in Correspondence, and one that the device uses to subscribe to schedules of specific rooms and days.

But that also means that the view model cannot use its usual query pattern. It must create the facts representing the schedules it wishes to display. And fact creation is asynchronous. Hence, it cannot be done in a property.

To get around this, a ScheduleCreator asynchronously creates an array of Schedules for a given date. It is used as part of an AsyncJob within the Device. An AsyncJob observes a starting point, in this case a list of ScheduleCreators initialized with the correct parameters. It then runs an asynchronous computation and stores the result in an Observable (I mean, Independent; we are still stuck in UpdateControls land at this time). While we are waiting for the async operation to complete, the observable starts with an empty array of Schedules.

Now the view model can take a dependency upon the results of this async job. It will immediately get back the empty array, but will raise property changed when the ScheduleCreator finishes creating the Schedules asynchronously.

And, by the way, it's not just for view models. This async job is also used by the subscription, which is why this class is in the Device.   

# The Admin Portal
The admin portal is a WPF application that a venue administrator uses to schedule rooms, groups, and events. The portal can be run on any Windows machine, and it will synchronize with other admin portals and sign devices.

## Synchronization Service
In a brand new Correspondence application, the SynchronizationService initializes the Community. In this case, the Device has been given that responsibility. So the SynchronizationService in this application delegates to the Device. But it still takes on the responsibility of setting up storage and communication strategies.

The SynchronizationService exposes all of its interesting properties for use in the ViewModelLocator.

## View Model Locator
The ViewModelLocator creates and initializes the SynchronizationService. It then exposes a property for each view model, using the ViewModel method to create the wrapper that implements INotifyPropertyChanged. In the case of the admin application, that's just one view model: the MainViewModel.

## Main View Model
The MainViewModel represents the entire admin screen, including the list of rooms, and the date selector. It stores the selected date in the DateSelectionModel, so that the AdminDevice can use it to subscribe to the correct Schedules.

## Editor Models and Dialogs
When the user edits an event or a group in a modal dialog, the view model copies the event or group data into an EventEditorModel or GroupEditorModel. These are exposed through the EventEditorViewModel or GroupEditorViewModel.

# The Fake Device

## View Model Locator
## Main View Model
## Room Selector View Model
## Display View Model

# Real Devices

## Screen Controller
The ScreenController class determines which screen should be displayed on a device. It exposes that as the MainScreen property. If a room is selected, it returns a DisplayScreen. Otherwise, it returns a RoomSelectorScreen. When the user locks the device from the room selector screen, that action will change the SelectedRoom property of the device, triggering this property to change. 

## View Selector
The ViewSelector takes a dependency upon the ScreenController. It subscribes to the MainScreen property, and resolves a new view whenever it changes. It creates the appropriate kind of view based on the type of screen, and sets the view's BindingContext. Xamarin data binding takes it from there.

## Dependency
The Dependency folder is full of stuff that will ultimately be moved into the Assisticant library (the new name for UpdateControls). This includes the Subscribe extension method, which sets up a subscription on a Dependent field. When the Dependent changes, an action will be called.

It also includes a MakeCommand class. This creates ICommands that, like DelegateCommand or RelayCommand, forward their Execute methods to a delegate. But MakeCommand also lets you define a When clause that returns true when the command is enabled. It raises CanExecuteChanged when that clause changes.

And finally, it includes the ViewModelBase class. In an ideal UpdateControls (or Assisticant) application, you don't need to use this base class; the wrapper takes care of it for you. But there's still some work necessary to port that wrapper to a Xamarin.Forms-compatible portable class libraries. So in the meantime, use this base class to implement INotifyPropertyChanged. Call the Get or GetCollection method in a property getter to set up dependency tracking. If any observable field referenced by the lambda changes, then ViewModelBase will raise PropertyChanged. For collections, it will manage an ObservableCollection instead of raising PropertyChanged.