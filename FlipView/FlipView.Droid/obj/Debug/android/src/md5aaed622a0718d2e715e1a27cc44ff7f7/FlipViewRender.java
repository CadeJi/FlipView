package md5aaed622a0718d2e715e1a27cc44ff7f7;


public class FlipViewRender
	extends md5282f1122c1313907b9bf274dd2c2f344.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("FlipView.Droid.Renders.FlipViewRender, FlipView.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FlipViewRender.class, __md_methods);
	}


	public FlipViewRender (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == FlipViewRender.class)
			mono.android.TypeManager.Activate ("FlipView.Droid.Renders.FlipViewRender, FlipView.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public FlipViewRender (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == FlipViewRender.class)
			mono.android.TypeManager.Activate ("FlipView.Droid.Renders.FlipViewRender, FlipView.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public FlipViewRender (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == FlipViewRender.class)
			mono.android.TypeManager.Activate ("FlipView.Droid.Renders.FlipViewRender, FlipView.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
