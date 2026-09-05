# FlightBookingSystem

This repository contains a sample microservices-based Flight Booking System implemented using Clean Architecture principles. It is intended as a basic, educational example to demonstrate service boundaries, simple messaging between services using MassTransit/RabbitMQ, and data access using Dapper. This project is for communication and learning purposes only and is not production-ready.

---

## High level overview

- Projects target .NET 8.
- Architecture follows Clean Architecture (separated Core / Application / Infrastructure / API layers per service).
- Uses Dapper for lightweight SQL access in infrastructure projects.
- Uses MassTransit with RabbitMQ for inter-service messaging and asynchronous workflows.
- Uses mediator patterns to decouple request handling inside services.

---

## Folder / Project structure (top-level)

The repository is organized by microservice and building blocks. At the root you will see a `BuildingBlocks` project and a set of service projects:

- `BuildingBlocks/FlightBookingSystem.BuildingBlocks` - common contracts, constants, and shared types used across services (message contracts, event bus constants, etc.).
- `Services/Booking` - Booking service projects:
  - `FlightBookingSystem.Booking.Core` (domain/entities/interfaces)
  - `FlightBookingSystem.Booking.Application` (use cases / handlers)
  - `FlightBookingSystem.Booking.Infrastructure` (Dapper repositories, db access)
  - `FlightBookingSystem.Booking.Api` (web API)
- `Services/Payment` - Payment service projects (Core / Application / Infrastructure / Api)
- `Services/Flight` - Flight service projects (Core / Application / Infrastructure / Api)
- `Services/Notification` - Notification service projects (Core / Application / Infrastructure / Api)

Each service follows the same pattern: `*.Core` contains entities and repository interfaces; `*.Application` contains handlers/commands/consumers; `*.Infrastructure` contains the concrete implementations (Dapper repositories, messaging setup); `*.Api` contains the web/external entrypoints.

---

## Architecture pattern

This project follows Clean Architecture concepts with clear separation of concerns:

- Core: domain entities, interfaces and contracts (no infrastructure dependencies).
- Application: use-cases, business rules, mediator handlers, and message consumers.
- Infrastructure: concrete implementations for data access (Dapper), messaging (MassTransit), and external integrations.
- API: controllers and Program/Startup wiring to expose HTTP endpoints.

Shared contracts and messaging constants live inside the `BuildingBlocks` project so services can communicate using a common message schema.

---

## Communication flow (event-driven)

1. Payment service publishes a `PaymentProcessed` event when a payment completes.
2. Notification service consumes the `PaymentProcessed` event (`PaymentProcessedConsumer`). The consumer maps the event to a `SendNotificationCommand` and forwards it to the application layer using the mediator.
3. Notification service sends the notification (email/SMS) using `NotificationService` and then publishes a `NotificationSent` event using MassTransit's `IPublishEndpoint`.
4. Booking service consumes `NotificationSent` and updates booking state or triggers follow-up actions.

This pattern decouples payment processing from notification sending and booking updates, allowing each service to evolve and scale independently.

---

## Request flow (synchronous HTTP interactions)

- External clients call HTTP APIs (e.g., `Payment.Api` to start a payment, `Booking.Api` to create bookings).
- API layer validates requests and forwards them to application handlers (commands/queries) which encapsulate business logic.
- Application layer interacts with Core repository abstractions implemented in Infrastructure using Dapper.
- For cross-service coordination, publish domain events rather than calling other services directly.

---

## Important notes

This repository is intentionally simple and aimed at demonstrating messaging and microservice communication. It is not production-ready. Use it for learning and prototyping; do not use it as-is in production systems.

---

## Recommended improvements (next steps to make it production-ready)

1. Reliability and messaging
   - Implement the Outbox pattern to ensure atomicity between database changes and published messages.
   - Add retry policies, circuit breakers, and dead-letter queues. Handle poison messages gracefully.
   - Add message contract versioning and a schema evolution strategy.
2. Idempotency
   - Make consumers idempotent and include deduplication where necessary.
3. Observability
   - Add structured logging, correlation IDs, distributed tracing (OpenTelemetry), and centralized metrics/monitoring.
4. Security
   - Add authentication and authorization (OAuth2/OpenID Connect) for all APIs.
   - Secure secrets with a secret store (Azure Key Vault, HashiCorp Vault) and encrypt sensitive data in transit and at rest.
5. Resilience and scalability
   - Add circuit breakers, bulkheads, timeouts, and backpressure controls.
6. Testing
   - Add unit tests, integration tests, message contract/consumer tests, and end-to-end tests.
7. CI/CD and deployment
   - Add Dockerfiles, docker-compose and/or Kubernetes manifests / Helm charts, and CI/CD pipelines for automated builds and deployments.
8. Database and migrations
   - Add database migration tooling (Flyway/DbUp/EF Migrations) and seed/test data.
9. Configuration and environment management
   - Centralize configuration, support per-environment settings, and use feature flags.
10. Documentation and API contracts
   - Add OpenAPI/Swagger for HTTP APIs and document message contracts for teams.
11. Performance and optimization
   - Profile critical paths, optimize Dapper queries, use connection pooling and caching where appropriate.
12. Code quality
   - Add static analysis, linters, code formatting, dependency update strategy, and address technical debt.

---