using AcmePayAssessment.BusinessLayer.Abstract;
using AcmePayAssessment.BusinessLayer.DTOs;
using AcmePayAssessment.DataAccessLayer.Abstract;
using AcmePayAssessment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcmePayAssessment.BusinessLayer.Concretes
{
    public class TransactionsManager : ITransactionService
    {
        private readonly IRepository<Transactions> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public TransactionsManager(IRepository<Transactions> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Response Authorize(AuthorizeAdd item)
        {
            var authorize = _repository.Add(new Transactions
            {
                Id = Guid.NewGuid(),
                PaymentId = Guid.NewGuid(),
                Amount = item.Amount,
                CardHolder = item.CardHolder,
                Currency = item.Currency,
                CVV = item.CVV,
                ExpirationMonth = item.ExpirationMonth,
                ExpirationYear = item.ExpirationYear,
                HolderName = item.HolderName,
                OrderReference = item.OrderReference,
                Status = Entity.Enums.Statuses.Authorized
            });

            _unitOfWork.Commit();
            return new Response
            {
                Id = authorize.PaymentId.ToString(),
                Status = Entity.Enums.Statuses.Authorized
            };
        }

        public Response Capture(Guid id, string orderReference = "")
        {
            var item = _repository.Get(v => v.PaymentId == id && v.OrderReference == orderReference).FirstOrDefault();
            if (item == null)
                throw new Exception("There is no transaction");
            item.Status = Entity.Enums.Statuses.Captured;

            _repository.Update(item);
            _unitOfWork.Commit();

            return new Response()
            {
                Id = id.ToString(),
                Status = Entity.Enums.Statuses.Captured
            };
        }

        public Response Void(Guid id, string orderReference = "")
        {
            var item = _repository.Get(v => v.PaymentId == id && v.OrderReference == orderReference).FirstOrDefault();
            if (item == null)
                throw new Exception("There is no transaction");

            item.Status = Entity.Enums.Statuses.Voided;

            _repository.Update(item);
            _unitOfWork.Commit();

            return new Response()
            {
                Id = id.ToString(),
                Status = Entity.Enums.Statuses.Voided
            };
        }

        public List<TransactionListItem> GetTransactionList()
        {
            List<TransactionListItem> listItem = _repository.Get().Select(s => new TransactionListItem
            {
                Amount = s.Amount,
                CardHolder = s.CardHolder.Substring(0, 6).PadRight(s.CardHolder.Length - 4, '*') + s.CardHolder.Substring(s.CardHolder.Length - 4, 4),
                Currency = s.Currency,
                HolderName = s.HolderName,
                Id = s.Id,
                OrderReference = s.OrderReference,
                PaymentId = s.PaymentId,
                Status = s.Status
            }).ToList();

            return listItem;

        }
    }
}
