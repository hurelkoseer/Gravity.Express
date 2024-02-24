namespace Gravity.Express.Application.Common;

public static class ErrorCodes
{
    public const string MustBeBiggerThenZero = "must_be_bigger_then_zero";

    public const string UserNotFound = "user_not_found";

    public const string PhoneCodeNotValid = "phone_code_not_valid";

    public const string AmountTooLarge = "amount_too_large";

    public const string NotExistPartnerCountry = "partner_has_no_operation_in_country";

    public const string CountryNotFound = "country_not_found";

    public const string IdempotentKeyInUse = "idempotency_key_in_use";

    public const string TransactionRateLimitExceeded = "transaction_rate_limit_exceeded";

    public const string TenantNotFound = "tenant_not_found";

    public const string CountryCodeIsRequired = "country_code_is_required";

    public const string PhoneNumberIsRequired = "phone_number_is_required";
}
