using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Zhao.Powers;

public abstract class ZhaoFormPower : PowerModel
{
	public override PowerType Type => (PowerType)1;

	public override PowerStackType StackType => (PowerStackType)2;

	public override int DisplayAmount => 0;
}
