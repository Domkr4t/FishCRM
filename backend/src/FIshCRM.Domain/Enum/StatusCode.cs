namespace FIshCRM.Domain.Enum
{
    public enum StatusCode
    {
        FishBaseAlreadyExists = 1,
        FishBaseNotFound = 2,

        FishAlreadyExists = 10,
        FishNotFound = 11,

        Ok = 200,
        InternalServerError = 500
    }
}
