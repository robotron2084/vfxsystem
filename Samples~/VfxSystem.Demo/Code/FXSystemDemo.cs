using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamStarterKit.FXSystem.Demo
{
    public class FXSystemDemo : MonoBehaviour
    {
        public FXSpawner Spawner;
        public TMP_Text InfoLabel;
        public Button DespawnButton;
        public Button DespawnAnimationButton;

        void Awake()
        {
            SetDespawnButtonsActive(false);
        }

        public void SpawnAndForgetTimeoutFx()
        {
            Spawner.SpawnAndForget("TimeoutUnit");
            InfoLabel.text = "SpawnAndForget() will create an FXUnit in a 'fire and forget' fashion. This FXUnit has a Timeout, so it despawns after 5 seconds. It is not 'retained' by the spawner.";
        }

        public void SpawnAndRetainTimeoutFX()
        {
            Spawner.SpawnAndRetain("TimeoutUnit");
            InfoLabel.text = "SpawnAndRetain() will create an FXUnit and retain it so that you can get access to it later using the key defined in the Spawner. This unit will timeout after 5 seconds and can only be retrieved during that time.\n\n Other useful functions:\n GetRetainedUnit()\n IsRetained()\n DespawnAndRelease()";
        }

        public void SpawnAndForgetManualFx()
        {
            Spawner.SpawnAndForget("ManualUnit");
            InfoLabel.text = "If an FXUnit's DespawnType is set to 'Manual', that means it expects some other code to Despawn() it. Since this unit is set up to Spawn And Forget, you must store a reference to the FXUnit when you spawn it. In this example, this unit has leaked, and you'll have to restart the demo to remove it!";
        }

        public void SpawnAndRetainManualFx()
        {
            Spawner.SpawnAndRetain("ManualUnit");
            SetDespawnButtonsActive(true);
            InfoLabel.text = "If an FXUnit's DespawnType is set to 'Manual', that means it expects some other code to Despawn() it. Since this unit is set up to SpawnAndRetain(), a reference to the spawned unit is retained by the spawner, so you can get access to it later. You can manually despawn the code via the Despawn() button.";
        }

        public void DespawnManual()
        {
            Spawner.DespawnAndRelease("ManualUnit");
            SetDespawnButtonsActive(false);
            InfoLabel.text = "FxSpawner.DespawnAndRelease() will immediately despawn your unit and it will no longer be retained by the Spawner. This can feel abrupt. You may want to instead use an animator to smoothly transition out your vfx with DespawnViaAnimation().";
        }

        public void DespawnViaAnimation()
        {
            Spawner.DespawnViaAnimation("ManualUnit", AnimationParameter.Bool("TransitionOut", true));
            SetDespawnButtonsActive(false);
            InfoLabel.text = "FxSpawner.DespawnViaAnimation() is a helper method to set an animation parameter and release a retained FXUnit from the spawner. An animator on an FXUnit can then call Animation_Despawn() in order to despawn the unit once the animation has played out.";
        }

        private void SetDespawnButtonsActive(bool newValue)
        {
            DespawnButton.gameObject.SetActive(newValue);
            DespawnAnimationButton.gameObject.SetActive(newValue);
        }

    }
}