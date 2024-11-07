using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour {
    // 파티클 오브젝트 변수
    public GameObject negativeParticles;
    public GameObject positiveParticles;

    private void Start() {
        // 파티클 오브젝트 비활성화
        negativeParticles.SetActive(false);
        positiveParticles.SetActive(false);
    }

    // 파티클 재생
    public void PlayParticle(bool isPositive) {
        // 파티클 오브젝트 활성화
        if (isPositive) {
            positiveParticles.SetActive(true);
        }
        else {
            negativeParticles.SetActive(true);
        }
    }

    // 파티클 정지
    public void StopParticle() {
        // 파티클 오브젝트 비활성화
        positiveParticles.SetActive(false);
        negativeParticles.SetActive(false);
    }
}
