# Qserve 10.1 — Phase 4 (Business Logic)

## Implemented Scope
- Customer Screen support logic:
  - Generate queue
  - Queue calculation
  - Current number
  - Waiting count
  - Printing
- Employee actions:
  - Transfer (service/counter)
  - Keep number or generate new number
  - Counter assignment
  - Queue reset
- Validation and state-guard rules.

## Key Components
- `QueueWorkflowService` orchestrates queue generation, transfer, counter assignment, display updates, and reset.
- `QueueValidationService` centralizes validation rules and invalid state prevention.
- `QueueNumberFormatter` enforces queue number format.
- Contracts:
  - `IQueueRepository` (persistence abstraction)
  - `IDisplayUpdater` (UI display update abstraction)
  - `ITicketPrinter` (ticket printing abstraction)

## Rule Coverage
- Generate queue number as `PREFIX-####`.
- Print ticket after generation.
- Update display and employee screen after queue-affecting actions.
- Transfer supports:
  - changing service
  - assigning counter
  - keeping ticket number
  - generating new number
- Prevent invalid transitions (completed/cancelled cannot transfer or be reassigned).

## Exclusions
- No installer changes.
- No UI/XAML changes in this phase.
- No concrete database implementation in this phase.
