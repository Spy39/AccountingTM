using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Enums
{
    public enum ApplicationStatus
    {
        /// <summary>
        /// Новая
        /// </summary>
        New,
        /// <summary>
        /// Получен комментарий
        /// </summary>
        CommentReceived,
        /// <summary>
        /// Комментарий отправлен
        /// </summary>
        CommentSent,
        /// <summary>
        /// В работе
        /// </summary>
        InProgress,
        /// <summary>
        /// Приостановлена
        /// </summary>
        Suspended,
        /// <summary>
        /// Передана
        /// </summary>
        Transferred,
        /// <summary>
        /// Решена
        /// </summary>
        Solved
    }
}
