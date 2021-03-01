using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.View
{

    public class StoreWindow : BaseWindow
    {
        public StoreWindow()
        {
            resName = "UI/Window/StoreWindow";
            resident = true;
            windowType = WindowType.StoreWindow;
            scenesType = ScenesType.Login;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnAddListener()
        {
            base.OnAddListener();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnRemoveListener()
        {
            base.OnRemoveListener();
        }

        protected override void RegisterUIEvent()
        {
            base.RegisterUIEvent();
            foreach (var button in buttonList)
            {
                switch (button.name)
                {
                    case "Button":
                        button.onClick.AddListener(OnBuyButtonClick);

                        break;
                }
            }

        }

        public override void Update(float dateTime)
        {
            base.Update(dateTime);
            if (Input.GetKeyDown(KeyCode.C))
            {
                Close();
            }
        }


        private void OnBuyButtonClick()
        {
            Debug.Log("点击了");
            //Test
            StroeCtrl.Instance.SavaPro(new Prop());
            var prop = StroeCtrl.Instance.GetProp(1001);
        }
    }
}