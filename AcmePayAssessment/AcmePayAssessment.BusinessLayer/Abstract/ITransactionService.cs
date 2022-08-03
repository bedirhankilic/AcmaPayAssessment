using AcmePayAssessment.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;

namespace AcmePayAssessment.BusinessLayer.Abstract
{
    public interface ITransactionService
    {
        Response Authorize(AuthorizeAdd item);
        Response Void(Guid id, string orderReference = "");
        Response Capture(Guid id, string orderReference = "");
        List<TransactionListItem> GetTransactionList();
    }
}
