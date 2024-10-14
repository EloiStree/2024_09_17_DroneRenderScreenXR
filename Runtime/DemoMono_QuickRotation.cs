using UnityEngine;


namespace Eloi.RenderScreen { 
    public class DemoMono_QuickRotation : MonoBehaviour
    {
        public Transform m_whatToRotate;
        public Vector3 m_rotationEuler = new Vector3(1, 1, 1);
        public Space m_space = Space.World;

        public float m_multiplicator=360f;

        private void Reset()
        {
                m_whatToRotate = transform;
        
        }


        private void Update()
        {
            if(m_whatToRotate!=null)
                m_whatToRotate.Rotate(m_rotationEuler * Time.deltaTime * m_multiplicator, m_space);
        }
    }
}
