using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Pipes
{
    public class Result<TSuccess, TFailure>
    {
        private readonly bool _isSuccess;

        private Result(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }

        private Result(bool isSuccess, TFailure failureValue)
        {
            _isSuccess = isSuccess;
            FailureValue = failureValue;
        }

        public TSuccess SuccessValue { get; private set; }
        public TFailure FailureValue { get; private set; }
        public bool IsSuccess { get { return _isSuccess; } }

        public static Result<TNewSuccess, TFailure> SucceedWith<TNewSuccess>(TNewSuccess value) =>
            new Result<TNewSuccess, TFailure>(true)
            {
                SuccessValue = value
            };

        public static Result<TNewSuccess, TFailure> FailWith<TNewSuccess>(TFailure value) =>
            new Result<TNewSuccess, TFailure>(false)
            {
                FailureValue = value
            };

        public static Result<TSuccess, TFailure> FailWith(TFailure value) =>
            new Result<TSuccess, TFailure>(false)
            {
                FailureValue = value
            };

        public Result<TSuccess, TFailure> Bind(Func<TSuccess, Result<TSuccess, TFailure>> fn) =>
            IsSuccess
                ? fn(SuccessValue)
                : this;


        public Result<TResultSuccess, TFailure> Finishing<TResultSuccess>
            (Func<TSuccess, TResultSuccess> execute, Action<TFailure> onFailure)
        {
            if (IsSuccess)
                return SucceedWith(execute(SuccessValue));

            onFailure(FailureValue);
            return FailWith<TResultSuccess>(FailureValue);
        }


        public Result<TSuccess, TFailure> Success(Func<TSuccess, Result<TSuccess, TFailure>> fn) =>
            IsSuccess
                ? fn(SuccessValue)
                : this;

        public Result<TSuccess, TFailure> Success(Action<TSuccess> fn)
        {
            if (IsSuccess)
                fn(SuccessValue);
            return this;
        }

        public Result<TSuccess, TFailure> Failure(Func<TFailure, Result<TSuccess, TFailure>> fn) =>
            IsSuccess
                ? this
                : fn(FailureValue);

        public Result<TSuccess, TFailure> Failure(Action<TFailure> fn)
        {
            if (!IsSuccess)
                fn(FailureValue);
            return this;
        }

        public Result<TSuccess, TFailure> Failure(Action fn)
        {
            if (!IsSuccess)
                fn();
            return this;
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
