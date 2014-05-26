﻿using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Mvc.Models
{
    /// <summary>
    /// This interface is used as a model for the DesignerController.
    /// </summary>
    public interface IDesignerModel
    {
        /// <summary>
        /// Gets the available designer views.
        /// </summary>
        IEnumerable<string> Views { get; }
    }
}
