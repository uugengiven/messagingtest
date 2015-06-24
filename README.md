# Message flow test for RabbitMQ (can be repurposed to any other message bus)

This repo has 4 separate solutions, a set up for the tests to match our current environment at work, a REST api that accepts posts and pushes the post directly to the message queue, a console app that processes message queues by dumping messages out one at a time as if it were doing work (with Ack) and a console app that generates a large number of messages against the REST api.

There is currently some hard coding in the generating app but messages are randomly selected between queues and can be different sizes. The test I was running needed to verify against large messages (64K+), although this test should be able to scale up beyond that size.

Multiple copies of each application other than the setup application can be run concurrently to simulate a horizontally scaled solution against a RabbitMQ backend.
