namespace FunApp.Services.MachineLearning
{
    using Microsoft.ML.Runtime.Api;
    using Microsoft.ML.Runtime.Data;

    internal class JokeModelPrediction
    {
        [ColumnName(DefaultColumnNames.PredictedLabel)]
        public string Category { get; set; }
    }
}
