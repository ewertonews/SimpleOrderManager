## OrderManager API

This is a simple API for adding orders a restaurant to different queues.
The app is functional but it is just a proof of concept and more work is yet to be done in order to have it deployed for a real use.

## Points of improvement

Currently this api is using a local (same process) queueing management API.  The ideal scenario would be using a sepate serve with a messaging service such as  **RabbitMQ** or **Kafka**. Another option would be using a background service.

For this current queuing mechanism, only adding to queues are working, but there still no an event handling or subscribing workflow to warn the queue message producers that an item have been added to the queue or processed.

It is not multi tenant ready: some additions would have to be done to the solution in order to have it deployed in the cloud for a multitenancy approch.

Some tests (units and integration) are still yet to be written (coded). The time constraints didn't allow it to happen up to this point in time (document writting).
