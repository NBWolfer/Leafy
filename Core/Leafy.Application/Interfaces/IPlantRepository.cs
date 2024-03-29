﻿using Leafy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Interfaces
{
    public interface IPlantRepository
    {
        public Task<List<Plant>> GetPlantWithDisease(); 
        public Task<string> ScanPlantDisase(string Image);
        public Task<Plant> GetPlantByName(string name);
    }
}
