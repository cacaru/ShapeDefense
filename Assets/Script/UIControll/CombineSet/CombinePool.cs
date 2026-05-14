using System.Collections.Generic;
using UnityEngine;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

namespace ShapeDefenseSpace {
    public class CombinePool : Singleton<CombinePool>
    {
        private readonly Queue<GameObject> function_title_queue = new();
        private readonly Queue<GameObject> function_2_queue = new();
        private readonly Queue<GameObject> function_3_queue = new();
        private readonly Queue<GameObject> function_4_queue = new();
        private readonly Queue<GameObject> function_5_queue = new();

        private readonly string title_name = "combine_title";
        private readonly string function_2_name = "combine_2";
        private readonly string function_3_name = "combine_3";
        private readonly string function_4_name = "combine_4";
        private readonly string function_5_name = "combine_5";

        private readonly int init_count = 20;

        private void Start() {
            Initialize(init_count);
        }

        private void CreateCombineAll() {
            var title = Instantiate(_title, transform);
            title.name = title_name;
            title.SetActive(false);
            function_title_queue.Enqueue(title);

            var function_2 = Instantiate(_function_2, transform);
            function_2.name = function_2_name;
            function_2.SetActive(false);
            function_2_queue.Enqueue(function_2);

            var function_3 = Instantiate(_function_3, transform);
            function_3.name = function_3_name;
            function_3.SetActive(false);
            function_3_queue.Enqueue(function_3);

            var function_4 = Instantiate(_function_4, transform);
            function_4.name = function_4_name;
            function_4.SetActive(false);
            function_4_queue.Enqueue(function_4);

            var function_5 = Instantiate(_function_5, transform);
            function_5.name = function_5_name;
            function_5.SetActive(false);
            function_5_queue.Enqueue(function_5);
        }
        private void CreateTitle() {
            var title = Instantiate(_title, transform);
            title.name = title_name;
            title.SetActive(false);
            function_title_queue.Enqueue(title);
        }

        private void CreateFunction2() {
            var function_2 = Instantiate(_function_2, transform);
            function_2.name = function_2_name;
            function_2.SetActive(false);
            function_2_queue.Enqueue(function_2);
        }

        private void CreateFunction3() {
            var function = Instantiate(_function_3, transform);
            function.name = function_2_name;
            function.SetActive(false);
            function_3_queue.Enqueue(function);
        }

        private void CreateFunction4() {
            var function = Instantiate(_function_4, transform);
            function.name = function_2_name;
            function.SetActive(false);
            function_4_queue.Enqueue(function);
        }

        private void CreateFunction5() {
            var function = Instantiate(_function_5, transform);
            function.name = function_2_name;
            function.SetActive(false);
            function_5_queue.Enqueue(function);
        }

        private void Initialize(int count) {
            for(int i = 0; i < count; i++) {
                CreateCombineAll();
            }
        }

        public GameObject GetFuction(int type) {
            GameObject result = type switch {
                1 => function_title_queue.Dequeue(),
                2 => function_2_queue.Dequeue(),
                3 => function_3_queue.Dequeue(),
                4 => function_4_queue.Dequeue(),
                5 => function_5_queue.Dequeue(),
                _ => null
            };
            result.SetActive(true);
            return result;
        }

        public void ReturnFunction(GameObject function, int type) {

            function.transform.SetParent(transform);
            function.SetActive(false);

            switch (type) {
                case 1:
                    function_title_queue.Enqueue(function);
                    break;
                case 2:
                    function.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);

                    function.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = false;

                    function_2_queue.Enqueue(function);
                    break;
                case 3:
                    function.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);

                    function.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = false;

                    function_3_queue.Enqueue(function);
                    break;
                case 4:
                    function.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);

                    function.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = false;

                    function_4_queue.Enqueue(function);
                    break;
                case 5:
                    function.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("MaterialBack_4").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);
                    function.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(false, DEFAULT_STR);

                    function.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Material_4").GetComponent<CombineMaterialClick>().InLib = false;
                    function.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = false;

                    function_5_queue.Enqueue(function);
                    break;
            }
        }

        public void CheckingFunction() {
            int size;
            // 각 function이 20개씩 있는지 확인
            if(init_count != function_title_queue.Count) {
                size = Mathf.Abs(init_count - function_title_queue.Count);
                for (int i = 0; i < size; i++) {
                    CreateTitle();
                }
            }

            if(init_count != function_2_queue.Count) {
                size = Mathf.Abs(init_count -function_2_queue.Count);
                for(int i = 0; i < size; i++) {
                    CreateFunction2();
                }
            }

            if (init_count != function_3_queue.Count) {
                size = Mathf.Abs(init_count - function_3_queue.Count);
                for (int i = 0; i < size; i++) {
                    CreateFunction3();
                }
            }

            if (init_count != function_4_queue.Count) {
                size = Mathf.Abs(init_count - function_4_queue.Count);
                for (int i = 0; i < size; i++) {
                    CreateFunction4();
                }
            }

            if (init_count != function_5_queue.Count) {
                size = Mathf.Abs(init_count - function_5_queue.Count);
                for (int i = 0; i < size; i++) {
                    CreateFunction5();
                }
            }
        }
    }
}
