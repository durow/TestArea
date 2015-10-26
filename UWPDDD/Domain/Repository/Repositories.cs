
using Domain.Repository.Common;
using Infrastructure.Database;

namespace Domain.Repository
{
    public static class Repositories
    {

        public static DatabaseInfo DatabaseInfo { get; set; }
        private static IANoteRepository _aNoteRepository;

        public static IANoteRepository ANoteRepository
        {
            get
            {
                return _aNoteRepository;
            }
            set { _aNoteRepository = value; }
        }

        private static IACategoryRepository _aCategoryRepository;

        public static IACategoryRepository ACategoryRepository
        {
            get { return _aCategoryRepository; }
            set { _aCategoryRepository = value; }
        }


    }
}
