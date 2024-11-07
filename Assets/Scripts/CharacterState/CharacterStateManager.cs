using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateManager : MonoBehaviour {
    // 카드에 담긴 캐릭터 상태 증가/감소에 따라 슬라이더의 하이라이트 색상을 변경하는 함수


    // 매개변수로 받은 Enum_CharcterState에 따라 상태를 업데이트
    // public void UpdateCharacterState(Enum_CharcterState enum_CharcterState, int value) {
    //     CharacterStatus characterStatus = CharacterStatusDAO.GetCharacterStatus();
    //     switch (enum_CharcterState) {
    //         case Enum_CharcterState.HP:
    //             characterStatus.HP += value;
    //             break;
    //         case Enum_CharcterState.Intelligence:
    //             characterStatus.Intelligence += value;
    //             break;
    //         case Enum_CharcterState.Happiness:
    //             characterStatus.Happiness += value;
    //             break;
    //         case Enum_CharcterState.Charm:
    //             characterStatus.Charm += value;
    //             break;
    //         case Enum_CharcterState.Money:
    //             characterStatus.Money += value;
    //             break;
    //     }
    //     CharacterStatusDAO.UpdateCharacterStatus(characterStatus);
    // }
}
