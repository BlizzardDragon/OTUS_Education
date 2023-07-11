using System;
using Leopotam.EcsLite;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Services
{
    public struct UnpackEntityUtils
    {
        public static int UnpackEntity(EcsWorld world, EcsPackedEntity pack)
        {
            if (pack.Unpack(world, out int entity))
            {
                return entity;
            }
            else
            {
                throw new InvalidOperationException("Failed to unpack!");
            }
        }
    }
}