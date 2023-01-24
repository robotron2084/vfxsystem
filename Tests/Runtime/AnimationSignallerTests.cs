using System.Collections;
using System.Collections.Generic;
using GameJamStarterKit.FXSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace com.enemyhideout.fx.tests
{
  [TestFixture]
  public class AnimationSignallerTests
  {
    private Signaller _signaller;
    private SignallerTester _tester;
    private Animator _animator;
    
    [SetUp]
    public void Setup()
    {
      GameObject prefab = Resources.Load<GameObject>("TestCube");
      var animatedObject = Object.Instantiate(prefab);
      _signaller = animatedObject.GetComponent<Signaller>();
      _animator = animatedObject.GetComponent<Animator>();
      _tester = animatedObject.GetComponent<SignallerTester>();
    }
    
    [UnityTest]
    public IEnumerator TestRaiseSignal()
    {
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      _animator.SetBool("Visible", true);
      Assert.That(_tester.SignalReceived, Is.False);
      yield return new WaitForSeconds(1.0f);
      Assert.That(_tester.SignalReceived, Is.True);
      
    }

    [UnityTest]
    public IEnumerator TestRaiseSignalTwice()
    {
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      _animator.SetBool("Visible", true);
      Assert.That(_tester.SignalReceived, Is.False);
      yield return new WaitForSeconds(1.0f);
      Assert.That(_tester.SignalReceived, Is.True);
      _animator.SetBool("Visible", false);
      _tester.Reset();
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      yield return new WaitForSeconds(1.0f);

      Assert.That(_tester.SignalReceived, Is.True);
      
    }

  }
}