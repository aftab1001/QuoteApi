namespace DocuWare.Application.Features.Quote.Dtos;

public class BaseResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public void SetSuccess(bool success)
    {
        Success = success;
    }
}