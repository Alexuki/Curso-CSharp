using Microsoft.AspNetCore.Mvc;

[NonController]
public class VideoController
{
    public bool Shutdown()
    {
        //TODO: Shutdown device
        return true;
    }
}