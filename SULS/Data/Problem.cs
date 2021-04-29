using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Data
{
    public class Problem
    {

        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new List<Submission>();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }


        public int Points { get; set; }

        public ICollection<Submission> Submissions { get; set; }
       
    }
}