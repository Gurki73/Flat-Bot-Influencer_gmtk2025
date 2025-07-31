using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace CortexLoop
{
    public class ChordGraphController : ModuleBase, IChordGraph
    {
        public string ModuleName => string.IsNullOrWhiteSpace(moduleNameOverride) ? gameObject.name : moduleNameOverride;
        public ModulePriority Priority => priority;

        public override void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            var mockNeurons = new List<NeuronData>
            {
                new NeuronData { id = "n1", displayName = "Hero", state = NeuronState.Active },
                new NeuronData { id = "n2", displayName = "Setting", state = NeuronState.Inactive },
                new NeuronData { id = "n3", displayName = "Conflict", state = NeuronState.Active },
                new NeuronData { id = "n4", displayName = "Resolution", state = NeuronState.Locked },
                new NeuronData { id = "n5", displayName = "Villian", state = NeuronState.Active },
                new NeuronData { id = "n6", displayName = "McGuffin", state = NeuronState.Locked },
            };

            SpawnNeurons(mockNeurons);
            ConnectSynapses();
        }

        public GameObject neuronPrefab;
        public GameObject synapsePrefab;
        public int neuronCount = 5;

        public float wobblePhaseOffset = 8.4f;
        public float wobbleAmount = 5.5f;
        public float speed = 1f;

        private List<Transform> neuronPoints = new();
        private List<NeuronVisual> neuronVisuals = new List<NeuronVisual>();
        private List<SynapseVisual> synapseVisuals = new List<SynapseVisual>();

        void SpawnNeurons(List<NeuronData> neurons)
        {
            Random.InitState(12345);
            float radius = 9;

            for (int i = 0; i < neurons.Count; i++)
            {
                float randy = Random.Range(0.85f, 1.15f);
                float angleJitter = Random.Range(-8f, 8f) * Mathf.Deg2Rad;
                float angle = i * Mathf.PI * 2 / neurons.Count + angleJitter;

                Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius * randy;
                GameObject neuronObj = Instantiate(neuronPrefab, pos, Quaternion.identity, transform);

                var visual = neuronObj.GetComponent<NeuronVisual>();
                if (visual != null)
                {
                    visual.SetData(neurons[i]);
                    visual.id = neurons[i].id;
                    neuronVisuals.Add(visual);
                }

                neuronObj.name = $"Neuron_{i}";
                neuronPoints.Add(neuronObj.transform);
            }
        }

        void ConnectSynapses()
        {
            for (int i = 0; i < neuronPoints.Count; i++)
            {
                for (int j = 0; j < neuronPoints.Count; j++)
                {
                    if (i == j) continue;

                    GameObject synapse = Instantiate(synapsePrefab, transform);

                    Transform neuronA = neuronPoints[i];
                    Transform neuronB = neuronPoints[j];

                    Vector3 aPos = neuronA.position;
                    Vector3 bPos = neuronB.position;

                    Vector3 direction = (bPos - aPos).normalized;
                    Vector3 normal = new Vector3(-direction.y, direction.x, 0);

                    float signedOffset = 0.25f;

                    Vector3 start = aPos + normal * signedOffset;
                    Vector3 end = bPos + normal * signedOffset;

                    Vector3 middle = (start + end) / 2f;
                    middle += normal * Mathf.Sin(Time.time * speed + i * wobblePhaseOffset) * wobbleAmount;

                    var visual = synapse.GetComponent<SynapseVisual>();
                    if (visual != null)
                    {
                        visual.lineRenderer.positionCount = 2;
                        visual.lineRenderer.SetPosition(0, start);
                        visual.lineRenderer.SetPosition(1, end);

                        bool isActive = Random.value > 0.5f;
                        visual.lineRenderer.colorGradient = isActive ? visual.activeGradient : visual.idleGradient;

                        synapseVisuals.Add(visual);
                    }
                }
            }
        }

        IEnumerator HighlightSelectedNeurons(List<NeuronData> selectedNeurons)
        {
            foreach (var neuron in selectedNeurons)
            {
                NeuronVisual vis = null;
                foreach (var nv in neuronVisuals)
                {
                    if (nv.id == neuron.id)
                    {
                        vis = nv;
                        break;
                    }
                }
                vis?.Highlight();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
