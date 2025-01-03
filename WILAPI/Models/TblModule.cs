﻿using System;
using System.Collections.Generic;

namespace WILAPI.Models;

public partial class TblModule
{
    public string ModuleCode { get; set; } = null!;

    public string? ModuleName { get; set; }

    public virtual ICollection<TblStaffLecture> TblStaffLectures { get; set; } = new List<TblStaffLecture>();

    public virtual ICollection<TblStudentLecture> TblStudentLectures { get; set; } = new List<TblStudentLecture>();

    public virtual ICollection<TblUserModule> TblUserModules { get; set; } = new List<TblUserModule>();
}
