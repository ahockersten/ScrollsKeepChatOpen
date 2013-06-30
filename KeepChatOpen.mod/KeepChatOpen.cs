using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using ScrollsModLoader.Interfaces;
using UnityEngine;
using Mono.Cecil;

namespace KeepChatOpen.mod {
    public class KeepChatOpen : BaseMod {
        public KeepChatOpen() {
        }

        public static string GetName() {
            return "KeepChatOpen";
        }

        public static int GetVersion() {
            return 1;
        }

        public static MethodDefinition[] GetHooks(TypeDefinitionCollection scrollsTypes, int version) {
            try {
                return new MethodDefinition[] {
                    scrollsTypes["ChatUI"].Methods.GetMethod("Show", new Type[]{typeof(bool)})
                };
            }
            catch {
                Console.WriteLine("KeepChatOpen failed to connect to methods used.");
                return new MethodDefinition[] { };
            }
        }


        public override bool BeforeInvoke(InvocationInfo info, out object returnValue) {
            returnValue = null;
            if (info.target is ChatUI && info.targetMethod.Equals("Show")) {
                foreach (StackFrame frame in info.stackTrace.GetFrames()) {
                    // Disallow all except for:
                    // ChatUI.OnGUI() - this gets called when the user clicks the open/close button
                    // Lobby.Start() - this gets called when the user moves to the "arena" tab
                    if ((frame.GetMethod().ReflectedType.Equals(typeof(ChatUI)) && frame.GetMethod().Name.Contains("OnGUI")) ||
                        (frame.GetMethod().ReflectedType.Equals(typeof(Lobby)) && frame.GetMethod().Name.Contains("Start"))) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override void AfterInvoke(InvocationInfo info, ref object returnValue) {
            return;
        }
    }
}
