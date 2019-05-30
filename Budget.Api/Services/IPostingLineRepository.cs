using Budget.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Services
{
    public interface IPostingLineRepository
    {
        Boolean PostingLineExist(int accountId, int subAccountId);
        IEnumerable<PostingLine> GetPostingLines(int subAccountId);
        PostingLine GetPostingLine(int subAccountId, int postingLineId);
        PostingLine CreatePostingLine(int subAccountId, PostingLine postingLine);
        void UpdatePostingLine(int PostingLineId, SubAccount PostingLine);
        void DeletePostingLine(int PostingLineId);
    }

}
