namespace Leafy.Application.Features.Results.PlantResults
{
    public class GetPlantByIdQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DiseaseId { get; set; }
    }
}
