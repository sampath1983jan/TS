using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Services.ErrorHandling
{
    public class ErrorInfo
    {
        public enum ErrorType {
            _noerror = 0,
            _critical = 1,
            _warning = 2,
            _information = 3
        }
        public string ErrorMessage;
        public ErrorType Type;
        public ErrorInfo() {

        }
        public ErrorInfo(string errorMessage, ErrorType type)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            Type = type;
        }
    }

    public class ErrorInfoCollection : List<ErrorInfo>
    {
        public void Add(string errorMsg, ErrorInfo.ErrorType errorType) {
            this.Add(new ErrorInfo(errorMsg, errorType));
        }
        public bool HasCriticalError() {
            foreach (ErrorInfo err in this) {
                if (err.Type == ErrorInfo.ErrorType._critical) {
                    return true;
                }
            }
            return false;
        }

        public int CriticalCount() {
            int iCount = 0;
            foreach (ErrorInfo err in this)
            {
                if (err.Type == ErrorInfo.ErrorType._critical)
                {
                    iCount += 1;
                }
            }
            return iCount;
        }
    }
}
