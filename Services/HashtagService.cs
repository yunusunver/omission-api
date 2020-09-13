using System.Linq;
using omission.api.Context;
using omission.api.Models;

namespace omission.api.Services
{
    
    public class HashtagService {
        private OmissionContext _context;

        public HashtagService(OmissionContext context)
        {
            _context = context;
        }

        public Hashtag GetById(int id){
            return _context.Hashtags.FirstOrDefault(x=>x.Id==id);
            
        } 
    }
}