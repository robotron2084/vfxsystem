using System.Collections;
using System.Collections.Generic;
using GameJamStarterKit.FXSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace com.enemyhideout.fx.tests

{
  public class SignallerTests
  {
    private Signaller _signaller;
    private SignallerTester _tester;
    private SignallerTester _tester2;

    [SetUp]
    public void Setup()
    {
      GameObject obj = new GameObject();
      _signaller = obj.AddComponent<Signaller>();
      _tester = obj.AddComponent<SignallerTester>();
      _tester2 = obj.AddComponent<SignallerTester>();
    }
    
    [UnityTest]
    public IEnumerator TestRaiseSignal()
    {
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      Assert.That(_tester.SignalReceived, Is.False);
      yield return new WaitForSeconds(1.0f);
      _signaller.RaiseSignal(Signaller.Complete);

      yield return null;
      Assert.That(_tester.SignalReceived, Is.True);
    }

    [UnityTest]
    public IEnumerator TestRaiseSignalWithoutListeners()
    {
      _signaller.RaiseSignal(Signaller.Complete);
      yield return null;

      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      Assert.That(_tester.SignalReceived, Is.False);
      yield return new WaitForSeconds(1.0f);
      Assert.That(_tester.SignalReceived, Is.False);
    }

    [UnityTest]
    public IEnumerator TestWaitForSignalTwice()
    {
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));

      yield return new WaitForSeconds(1.0f);
      _signaller.RaiseSignal(Signaller.Complete);

      yield return null;
      Assert.That(_tester.SignalReceived, Is.True);
      _tester.Reset();
      
      //wait a frame, the signal is still valid for this frame.
      yield return null;
      
      yield return _tester.WaitForSignalOrTimeout(Signaller.Complete, 1.0f);
      
      Assert.That(_tester.SignalReceived, Is.False);
      
    }

    [UnityTest]
    public IEnumerator TestWaitForTwoDifferentSignals()
    {
      _tester.StartCoroutine(_tester.WaitForSignalOrTimeout(Signaller.Complete, 2.0f));
      _tester.StartCoroutine(_tester2.WaitForSignalOrTimeout("Signal2", 2.0f));

      yield return new WaitForSeconds(1.0f);
      _signaller.RaiseSignal(Signaller.Complete);
      _signaller.RaiseSignal("Signal2");

      yield return null;
      Assert.That(_tester.SignalReceived, Is.True);
      Assert.That(_tester2.SignalReceived, Is.True);
      
    }

  }
   
}