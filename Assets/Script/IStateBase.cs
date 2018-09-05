using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBase
{
    // 初期化
    void Init(Monster monster);

    // 更新
    void Update(Monster monster);

}
