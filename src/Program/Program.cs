using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using CognitiveCoreUCU;
using System.Drawing;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string imageName = "beer";
            PictureProvider picProvider = new PictureProvider();
            IPicture pic = picProvider.GetPicture($@"src\Program\{imageName}.jpg");

            IFilter filterNegative = new FilterNegative();
            IFilter filterGreyscale = new FilterGreyscale();
            IFilter filterSaveLocal1 = new FilterSaveLocal($@"{imageName}FilteredStep1.jpg");
            IFilter filterSaveLocal2 = new FilterSaveLocal($@"{imageName}FilteredStep2.jpg");
            IFilter filterSaveLocal3 = new FilterSaveLocal($@"{imageName}FilteredStep3.jpg");
            IFilter filterTwitterPublish = new FilterTwitterPublish($@"{imageName}FilteredStep3.jpg", "rj");
            
            IPipe pipeZ = new PipeNull();
            IPipe pipeT = new PipeSerial(filterTwitterPublish, pipeZ);
            IPipe pipeW = new PipeSerial(filterSaveLocal3, pipeT);
            IPipe pipeY = new PipeSerial(filterNegative, pipeW);
            IPipe pipeU = new PipeSerial(filterSaveLocal2, pipeY);
            IPipe pipeX = new PipeSerial(filterGreyscale, pipeU);
            IPipe pipeV = new PipeSerial(filterSaveLocal1, pipeX);

            IPicture picFiltered = pipeV.Send(pic);
            */

            string imageName = "beer";
            PictureProvider picProvider = new PictureProvider();
            IPicture pic = picProvider.GetPicture($@"src\Program\{imageName}.jpg");
            
            IFilter filterGreyscale = new FilterGreyscale();
            IFilterConditional filterConditional = new FilterHasFace($@"src\Program\{imageName}.jpg");
            IFilter filterSaveLocal = new FilterSaveLocal($@"filterOutput.jpg");
            IFilter filterTwitterPublish = new FilterTwitterPublish($@"filterOutput.jpg", "rj");
            IFilter filterNegative = new FilterNegative();

            IPipe pipe5 = new PipeNull();

            IPipe pipe7 = new PipeSerial(filterSaveLocal, pipe5);
            IPipe pipe6 = new PipeSerial(filterNegative, pipe7);

            IPipe pipe4 = new PipeSerial(filterTwitterPublish, pipe5);
            IPipe pipe3 = new PipeSerial(filterSaveLocal, pipe4);

            IPipe pipe2 = new PipeConditionalFork(pipe3, pipe6, filterConditional);
            IPipe pipe1 = new PipeSerial(filterGreyscale, pipe2);

            IPicture picFiltered = pipe1.Send(pic);
        }
    }
}
