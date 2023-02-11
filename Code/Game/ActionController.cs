using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryEngine.Projects.Game
{
    [EntityComponent(
            Category = "Entity Components"
        ,   Description = "Define Actions for the Action Manager"
        ,   Guid =  "F3057B02-C692-42EC-AA7B-533273BCA847"
        ,   Name = "Action Controller")]
    class ActionController : EntityComponent
    {
        private const string actionMapPath = "libs/config/defaultprofile.xml";
        private const string actionMapName = "player";
        private ActionHandler actionHandler = null;

        protected override void OnGameplayStart()
        {
            base.OnGameplayStart();

            actionHandler?.Dispose();

            actionHandler = new ActionHandler(actionMapPath, actionMapName);
            
            actionHandler.AddHandler("moveup", OnMoveUp);
        }

        void OnMoveUp(string name, InputState state, float value)
        {
            if (state == InputState.Pressed)
            {
                var physical_entity = Entity.Physics;
                if (physical_entity == null)
                    return;

                var living_status = physical_entity.GetStatus<LivingStatus>();
                if (living_status == null)
                    return;

                if (!living_status.IsFlying)
                    physical_entity.Jump(new Vector3(0.0f, 0.0f, 5.5f));
            }
        }
    }
}