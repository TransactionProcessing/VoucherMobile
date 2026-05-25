# VoucherMobile

VoucherMobile is a non customer-facing application for voucher providers to validate and redeem customer vouchers at payment time.

## Implemented scope

This repository contains the core .NET MVVM application logic for voucher-provider workflows:

- Secure provider login via `LoginViewModel`
- Voucher lookup (scan code input) and detail retrieval
- Voucher expiry checks and remaining balance visibility
- Partial and full voucher redemption
- Transaction detail submission for redemption
- Customer receipt dispatch through a receipt service abstraction

## Structure

- `VoucherMobile.Core` - Domain models, services, and MVVM view models
- `VoucherMobile.Core.Tests` - Focused xUnit tests for login, expiry handling, redemption, and receipts

## Build and test

```bash
dotnet test /home/runner/work/VoucherMobile/VoucherMobile/VoucherMobile.slnx
```
