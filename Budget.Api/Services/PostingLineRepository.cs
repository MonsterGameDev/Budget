using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Api.Entities;

namespace Budget.Api.Services
{
    public class PostingLineRepository : IPostingLineRepository
    {
        public PostingLine CreatePostingLine(int subAccountId, PostingLine postingLine)
        {
            throw new NotImplementedException();
        }

        public void DeletePostingLine(int PostingLineId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostingLine> GetPostingLines(int subAccountId)
        {
            throw new NotImplementedException();
        }

        public PostingLine GetPostingLine(int subAccountId, int postingLineId)
        {
            throw new NotImplementedException();
        }

        public bool PostingLineExist(int accountId, int subAccountId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePostingLine(int PostingLineId, SubAccount PostingLine)
        {
            throw new NotImplementedException();
        }
    }
}
