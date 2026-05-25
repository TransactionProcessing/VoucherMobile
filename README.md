# VoucherMobile

Skeleton .NET MAUI voucher application based on the `TransactionMobile` project structure, currently trimmed down to a basic local logon flow.

## Solution structure

- `VoucherMobile.App` - MAUI shell, pages, and app wiring
- `VoucherMobile.BusinessLogic` - login services and view model logic
- `VoucherMobile.BusinessLogic.Tests` - unit tests for the login flow

## Demo logon

The first cut uses an in-memory authenticator so the app can run without backend dependencies.

- Username: `demo`
- Password: `Password123!`
