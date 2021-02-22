using System.Collections.Generic;
using UnityEngine;

namespace UnityBase.Shooter
{
    public class GenerateFigures : MonoBehaviour
    {
        private float offset_Y = 1.0f;
        private int bound_X, bound_Z, product;
        private bool ableToCreate;

        [SerializeField] GameObject figureToGenerate = null;

        private GameObject figure;

        [SerializeField] int figureAmount = 0;
        [SerializeField] List<Vector3> listOfCoordinates;

        private void Start()
        {
            GetBounds();
            GenerateFigure();
        }

        private void GetBounds()
        {
            bound_X = (int)GetComponent<BoxCollider>().bounds.size.x;
            bound_Z = (int)GetComponent<BoxCollider>().bounds.size.z;
            product = bound_X * bound_Z;
        }

        private void GenerateFigure()
        {

            for (int i = 0; i < figureAmount; i++)
            {

                ableToCreate = true;

                Vector3 spawnPos = DefinePosition();

                if (listOfCoordinates.Count != product)
                {
                    for (int k = 0; k < listOfCoordinates.Count; k++)
                    {
                        if (spawnPos == listOfCoordinates[k])
                        {
                            ableToCreate = false;
                            i--;
                            break;
                        }
                    }
                }
                else
                {
                    return;
                }

                if (ableToCreate)
                {
                    figure = Instantiate(figureToGenerate, spawnPos, Quaternion.identity);
                    figure.transform.SetParent(transform);
                    listOfCoordinates.Add(spawnPos);
                }
            }
        }

        private Vector3 DefinePosition()
        {
            float x = Random.Range(-bound_X / 2, bound_X / 2 + 1);
            float z = Random.Range(-bound_Z / 2, bound_Z / 2 + 1);
            
            Vector3 position = new Vector3(transform.position.x + x, offset_Y, transform.position.z + z);

            return position;
        }
    }
}