﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace VisualNovelManager.Models
{
    public class VNList
    {
        [Key]
        public int ListId { get; set; }                 // Database ID

        [Required]
        public string UserId { get; set; }              // Which user in database created the list

        [Required]
        public string ListName { get; set; }            // Name of the created list

        public string ListDescription { get; set; }     // Brief Description of list contents

        [Required]
        public string List { get; set; }                // String of Visual Novels added to the list

    }

    public class VNListCreateViewModel
    {
        public string ListName { get; set; }

        public string ListDescription { get; set; }

        public string List { get; set; }
        public string ListSelection { get; set; }
    }
}