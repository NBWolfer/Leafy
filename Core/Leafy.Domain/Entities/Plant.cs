namespace Leafy.Domain.Entities
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
