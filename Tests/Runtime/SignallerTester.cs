using System.Collections;
using System.Collections.Generic;
using GameJamStarterKit.FXSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.enemyhideout.fx.tests
{
  public class SignallerTester : MonoBehaviour
  {
    public bool SignalReceived;
    
    private Signaller _signaller;
    public void Awake()
    {
      _signaller = GetComponent<Signaller>();
    }
    public bool SignalReturned;

    public class CoroutineRefCounter
    {
      
      public int CoroutinesStarted = 0;
      public int CoroutinesCompleted = 0;
      
      public CoroutineRefCounter(MonoBehaviour context)
      {
        Context = context;
      }
      public MonoBehaviour Context;
      public IEnumerator Coroutine;

      public void Start(IEnumerator enumerator)
      {
        CoroutinesStarted++;
        Context.StartCoroutine(enumerator);
      }

      public void NotifyCompleted()
      {
        CoroutinesCompleted++;
      }

      public bool Any()
      {
        return CoroutinesCompleted > 0;
      }
    }

    public IEnumerator WaitForSignalOrTimeout(string signal, float timeToWait)
    {
      
      Debug.Log($"Waiting for signal {Time.frameCount}");

      var enumerators = new List<IEnumerator>();
      var refCounter = new CoroutineRefCounter(this);
      
      IEnumerator waitSignal = WaitForSignalAndSetReceived(signal, refCounter);
      IEnumerator timeoutSignal = TimeoutRoutine(timeToWait, refCounter);
      
      refCounter.Start(waitSignal);
      refCounter.Start(timeoutSignal);
      yield return Any(refCounter);


    }

    public IEnumerator WaitForSignalAndSetReceived(string signal, CoroutineRefCounter refCounter)
    {
      yield return _signaller.WaitForSignal(signal);
      float frame = Time.frameCount;
      SignalReceived = true;
      refCounter.NotifyCompleted();
      Debug.Log("Signal Received"+Time.frameCount);
    }

    public IEnumerator TimeoutRoutine(float timeToWait, CoroutineRefCounter refCounter)
    {
      yield return new WaitForSeconds(timeToWait);
      refCounter.NotifyCompleted();
    }

    public IEnumerator Any(CoroutineRefCounter refCounter)
    {
      while (true)
      {
        if (refCounter.Any())
        {
          yield break;
        }
        
        yield return null;
      }
    }
    
    public void Reset()
    {
      StopAllCoroutines();
      SignalReceived = false;
    }
  }
}