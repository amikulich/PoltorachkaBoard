﻿namespace Poltorachka.Domain
{
    public interface IFactRepository
    {
        void Save(Fact fact);

        Fact GetById(int factId);
    }
}
