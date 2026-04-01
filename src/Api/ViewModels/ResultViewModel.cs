namespace app.Api.ViewModels;

public class ResultViewModel<T>
{
    public T Data { get; private set; }
    public IList<string>? Errors { get; set; } = new List<string>();

    public ResultViewModel(T data, List<string>? errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResultViewModel(List<string> errors) => Errors = errors;


    public ResultViewModel(string error) => Errors.Add(error);
}
