# VoucherMobile

VoucherMobile is a non customer-facing application for voucher providers to validate and redeem customer vouchers at payment time.

## Implemented scope

This repository contains:

- **VoucherMobile.Core**: Domain models, services, and MVVM view models for:
  - secure provider login
  - voucher lookup and detail retrieval
  - expiry checks and remaining balance visibility
  - partial/full redemption
  - transaction detail submission
  - customer receipt dispatch abstraction
- **VoucherMobile.App**: .NET MAUI UI elements that bind to the core MVVM workflows:
  - login screen
  - voucher lookup screen
  - redemption screen
  - voucher detail display and result/status output
- **VoucherMobile.Core.Tests**: Focused xUnit tests for login, expiry handling, redemption, and receipts.

## Build and test

```bash
dotnet test /home/runner/work/VoucherMobile/VoucherMobile/VoucherMobile.slnx
```
