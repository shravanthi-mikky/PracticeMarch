using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
