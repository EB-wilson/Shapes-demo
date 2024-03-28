using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Shapes.Logic
{
    /// <summary>
    /// 时间表事务的基类，提供了时间表执行任务的进度管理等相关API，行为抽象，待实现
    /// </summary>
    public abstract class Task: MonoBehaviour
    {
        /// <summary>
        /// 此任务进行的标准时间，若为0，则此任务将在一次更新中完成所有流程并结束；如果为负数，则此任务会永远运行下去，直到调用<see cref="finalize"/>停止此任务
        /// </summary>
        public float duration;

        /// <summary>
        /// 此任务的起始时间
        /// </summary>
        public float beginTime;

        /// <summary>
        /// 此任务是否已被暂停，当为true时，此任务的执行会被完全暂停
        /// </summary>
        public bool paused;

        /// <summary>
        /// 当前任务运行到的时间，通常不建议手动设置此值
        /// </summary>
        public float time;

        /// <summary>
        /// 任务执行进度的插值函数，输入一个从0到1的值，并产出一个在0到1之间的值
        /// </summary>
        public Func<float, float> interp = f => f;

        /// <summary>
        /// 此任务当前进行到的进度，已经过插值函数进行插值，若需要获取原始数据，请使用<see cref="progressNonInterp"/>
        /// </summary>
        public float progress => interp(progressNonInterp);

        /// <summary>
        /// 此任务当前是否已经完成
        /// </summary>
        public virtual bool isComplete => posted;

        protected bool initialized;

        private bool began;
        private bool posted;

        public float progressNonInterp => posted? 1: duration < 0? 0: (duration == 0? 1: time / duration);

        public virtual void update(float timeDelta)
        {
            if (!initialized) throw new AssertionException("task was not initialize yet", "internal error");
            if (paused) return;

            if (!began)
            {
                began = true;
                begin();
            }

            if (!isComplete)
            {
                time += timeDelta;
                action();
            }

            if (progressNonInterp >= 1)
            {
                time = duration;

                if (posted) return;
                posted = true;
                post();
            }
        }

        /// <summary>
        /// 使此任务立即完成并结束
        /// </summary>
        public virtual void finalize()
        {
            if (isComplete) return;

            time = duration;
            posted = true;
            post();
        }

        /// <summary>
        /// 重置此任务的状态，使任务恢复到开始之前的状态
        /// </summary>
        public virtual void reset()
        {
            paused = false;
            began = posted = false;
            time = 0;
        }

        /// <summary>
        /// 初始化函数，在任务开始使用之前调用
        /// </summary>
        public virtual void init()
        {
            reset();
            initialized = true;
        }

        /// <summary>
        /// 在任务开始处理时调用，此时任务进度刚好为0
        /// </summary>
        protected abstract void begin();
        /// <summary>
        /// 此任务随执行更新调用的行为，也是任务的主要工作
        /// </summary>
        protected abstract void action();
        /// <summary>
        /// 在任务执行完成后调用，此时任务进度刚好为1
        /// </summary>
        protected abstract void post();
    }
}