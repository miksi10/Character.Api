﻿namespace CharacterApi.BusinessLogic.Models
{
    public class ItemGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BonusStrength { get; set; }
        public int BonusAgility { get; set; }
        public int BonusIntelligence { get; set; }
        public int BonusFaith { get; set; }
    }
}
