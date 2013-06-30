using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

using ScrollsModLoader.Interfaces;
using UnityEngine;
using Mono.Cecil;

// FIXME check existence of methods, private variables etc and fail gracefully!

namespace KeepChatOpen.mod {
    public class KeepChatOpen : BaseMod {
        private bool debug = false;

        public KeepChatOpen() {
        }

        public static string GetName() {
            return "KeepChatOpen";
        }

        public static int GetVersion() {
            return 1;
        }

        public static MethodDefinition[] GetHooks(TypeDefinitionCollection scrollsTypes, int version) {
            return new MethodDefinition[] { };
        }


        public override bool BeforeInvoke(InvocationInfo info, out object returnValue) {
            returnValue = null;
            return false;
        }

        public override void AfterInvoke(InvocationInfo info, ref object returnValue) {
            return;
        }
    }
}