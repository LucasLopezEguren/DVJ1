﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour{

    public bool useSensor = false;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    Vector3 lastPosition;
    Transform _transform;
    [HideInInspector] public Rigidbody _rigidBody;
    void Start () {
        _transform = transform;
        lastPosition = _transform.position;
        _rigidBody = GetComponent<Rigidbody>();
        if (useSensor) {
            foreach(PlatformSensor sensor in GetComponentsInChildren<PlatformSensor>()){
                sensor.carrier = this;
            }
        }
    }
    void LateUpdate() {
        if (rigidbodies.Count > 0) {
            for ( int i = 0; i < rigidbodies.Count; i++ ){
                Rigidbody rb = rigidbodies[i];
                if (rb != null){
                    Vector3 velocity = (_transform.position - lastPosition);
                    rb.transform.Translate(velocity, _transform);
                }
            }
        }
        lastPosition = _transform.position;
    }

    void OnCollisionEnter(Collision collided) {
        if (collided.gameObject.tag != "Player") return;
        if (useSensor) return;
        Rigidbody rb = collided.collider.GetComponent<Rigidbody>();
        if (rb != null ){
            AddRigidBody(rb);
        }
    }
    
    void OnCollisionExit(Collision collided) {
        if (collided.gameObject.tag != "Player") return;
        if (useSensor) return;
        Rigidbody rb = collided.collider.GetComponent<Rigidbody>();
        if (rb != null ){
            RemoveRigidBody(rb);
        }
    }
    
    public void AddRigidBody(Rigidbody rb) {
        if(!rigidbodies.Contains(rb)){
            rigidbodies.Add(rb);
        }
    }
    public void RemoveRigidBody(Rigidbody rb) {
        if(rigidbodies.Contains(rb)){
            rigidbodies.Remove(rb);
        }
    }
}
