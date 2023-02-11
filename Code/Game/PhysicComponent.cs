using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryEngine.Projects.Game
{
    [EntityComponent(
            Name = "Physic Component"
        ,   Category = "Entity Components"
        ,   Description = "Entity's Physical Representation"
        ,   Guid = "75EA47CE-2789-4235-BC41-FA64A32ACA11")]
    class PhysicComponent : EntityComponent
    {
        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Can the Physical Entity use a Capsule?")]
        public bool CanUseCapsule
        {
            set
            {
                _can_use_capsule = value;
                Physicalize();
            }
            get => _can_use_capsule;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Physical Entity Radius")]
        public float Radius
        {
            set
            {
                _radius = value;
                Physicalize();
            }
            get => _radius;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Physical Entity Height")]
        public float Height
        {
            set
            {
                _height = value;
                Physicalize();
            }
            get => _height;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Ground Contact Epsilon")]
        public float GroundContactEpsilon
        {
            set
            {
                _ground_contact_epsilon = value;
                Physicalize();
            }
            get => _ground_contact_epsilon;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Physical Entity Mass")]
        public float Mass
        {
            set
            {
                _mass = value;
                Physicalize();
            }
            get => _mass;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Physical Entity Air Resistance")]
        public float AirResistance
        {
            set
            {
                _air_resistance = value;
                Physicalize();
            }
            get => _air_resistance;
        }

        [EntityProperty(Type = EntityPropertyType.Primitive, Description = "Physical Entity Air Control")]
        public float AirControl
        {
            set
            {
                _air_control = value;
                Physicalize();
            }
            get => _air_control;
        }

        bool    _can_use_capsule;
        float   _radius;
        float   _height;
        float   _ground_contact_epsilon;
        float   _mass;
        float   _air_resistance;
        float   _air_control;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Physicalize();
        }

        void Physicalize()
        {
            var living_physic = new LivingPhysicalizeParams();

            var living_radius = _radius * 0.5f;
            var living_height = _height * 0.25f;

            var player_dimensions = living_physic.PlayerDimensions;
            player_dimensions.UseCapsule = _can_use_capsule;
            player_dimensions.ColliderSize = new Vector3(living_radius, 0.0f, living_height);
            player_dimensions.PivotHeight = 0.0f;
            player_dimensions.ColliderHeight = living_radius + living_height + 0.1f;
            player_dimensions.GroundContactEpsilon = _ground_contact_epsilon;

            var player_dynamics = living_physic.PlayerDynamics;
            player_dynamics.Mass = _mass;
            player_dynamics.AirResistance = _air_resistance;
            player_dynamics.AirControlCoefficient = _air_control;

            Entity.Physics.Physicalize(living_physic);
        }
    }
}