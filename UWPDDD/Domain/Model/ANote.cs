

using Domain.Repository;

namespace Domain.Model
{
    public sealed class ANote
    {
        public int ID { get; set; }
        public string AddDateTime { get; set; }
        public string EditDateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ACategory Category { get; set; }

        public void UpdateContent()
        {
            Content = Repositories.ANoteRepository.GetContent(ID);
        }
    }
}
