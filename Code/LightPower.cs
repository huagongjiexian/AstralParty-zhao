using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Zhao;

/// <summary>
/// Buff「照小姐就是我的光！」。战斗规则与卡牌文本中简称为“光”。
/// </summary>
public class LightPower : PowerModel
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;
}
