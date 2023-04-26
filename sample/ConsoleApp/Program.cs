using Sample.Pipelines;
using Sample.Samples;

Books.Example();

CreateUserPipeline.Run();
CreateUserPipeline.Run("ciccio@buffo");

GamblingPipeline.Run(1, 10);
GamblingPipeline.Run(1, 100);

